using ApiDevBP.Entities;
using ApiDevBP.Mapper;
using ApiDevBP.Models;
using ApiDevBP.Services.Interfaces;
using ApiDevBP.Validation;
using ApiDevBP.Validation.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ApiDevBP.Services
{
    public class UserService : IUserService
    {
        IConfiguration _configuration;
        ILogger<UserService> _logger;
        protected readonly IAutoMapper _mapper;
        protected readonly ApiDevBPContext _context;
        protected readonly ForbbidenNames _forbbidenNames;
        public UserService(IConfiguration configuration, ILogger<UserService> logger, IAutoMapper mapper, ApiDevBPContext context, IOptions<ForbbidenNames> forbbidenNames)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _context = context;
            _forbbidenNames = forbbidenNames.Value;
        }

        #region Public Methods
        public async Task<ResponseResultModel> CreateUser(UserModel user)
        {
            try
            {
                ValidateUser(user);
                UserEntity userEntity = _mapper.Map <UserModel, UserEntity>(user);
                await _context.UserEntities.AddAsync(userEntity);
                await _context.SaveChangesAsync();
                return ResponseResultModel.Ok(_mapper.Map<UserEntity, UserModel>(userEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en UserService: Message {msg}. StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<ResponseResultModel> DeleteUser(UserModel user)
        {
            try
            {
                UserEntity userEntity = await _context.UserEntities.FirstOrDefaultAsync(x => x.Name == user.Name && x.Lastname == user.Lastname) ?? throw new ValidationException("User not exists");
                _context.UserEntities.Remove(userEntity);
                await _context.SaveChangesAsync();
                return ResponseResultModel.Ok(_mapper.Map<UserEntity, UserModel>(userEntity));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en UserService: Message {msg}. StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<ResponseResultModel> GetUsers()
        {
            try
            {
                List<UserEntity> userEntities = await _context.UserEntities.ToListAsync();
                return ResponseResultModel.Ok(_mapper.Map<List<UserEntity>, List<UserModel>>(userEntities));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error en UserService: Message {msg}. StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
                throw;
            }
        }

        public async Task<ResponseResultModel> UpdateUser(UserModel user)
        {
            try
            {
                ValidateUser(user);
                UserEntity userEntity = await _context.UserEntities.FirstOrDefaultAsync(x => x.Name == user.Name && x.Lastname == user.Lastname) ?? throw new ValidationException("User not exists");
                _context.Update(userEntity);
                await _context.SaveChangesAsync();
                return ResponseResultModel.Ok(_mapper.Map<UserEntity, UserModel>(userEntity));

            }
            catch (Exception ex)
            {
                _logger.LogError("Error en UserService: Message {msg}. StackTrace: {stackTrace}", ex.Message, ex.StackTrace);
                throw;
            }
        }
        #endregion

        #region Private Methods
        private void ValidateUser(UserModel user)
        {
            if (user == null)
            {
                throw new ValidationException("User cannot be null");
            }
            if (_forbbidenNames.Names.ToLower().Split(";").Contains(user.Name.ToLower()))
            {
                throw new ValidationException("Name " + user.Name + " is not allowed");
            }
            if (_forbbidenNames.LastNames.ToLower().Split(";").Contains(user.Lastname.ToLower()))
            {
                throw new ValidationException("Lastname " + user.Lastname + " is not allowed", user.Lastname);
            }
        }

        #endregion
    }
}