using ApiDevBP.Entities;
using ApiDevBP.Models;
using AutoMapper;
using Profile = AutoMapper.Profile;

namespace ApiDevBP.Mapper
{
    public class AutoMapperWrapper : IAutoMapper
    {
       
        public AutoMapperWrapper()
        {
            _mapper = new AutoMapper.Mapper(GetMapperConfiguration());
            
        }
        public IMapper _mapper { get; set; }


        public IEnumerable<TOutput> Map<TInput, TOutput>(IEnumerable<TInput> input)
        {
            return this._mapper.Map<IEnumerable<TInput>, IEnumerable<TOutput>>(input);
        }

        public TOutput Map<TInput, TOutput>(TInput input)
        {
            return this._mapper.Map<TInput, TOutput>(input);
        }

        public TOutput Map<TInput, TOutput>(TInput input, TOutput output)
        {
            return this._mapper.Map(input, output);
        }

        private MapperConfiguration GetMapperConfiguration()
        {
            return new MapperConfiguration(x =>
            {
                
                x.CreateMap<UserModel, UserEntity>();
                x.CreateMap<UserEntity, UserModel>();
            });
        }
    }
}
