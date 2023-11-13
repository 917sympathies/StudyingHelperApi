using AutoMapper;
using StudyingHelperApi.DataTransferObjects;
using StudyingHelperApi.Models;
using Task = StudyingHelperApi.Models.Task;

namespace StudyingHelperApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<Workspace, WorkspaceDto>();

            CreateMap<UserToCreationDto, User>();

            CreateMap<WorkspaceToCreationDto, Workspace>();

            CreateMap<TaskToCreateDto, Task>();
        }
    }
}
