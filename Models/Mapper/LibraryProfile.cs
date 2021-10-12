using AutoMapper;
using library_api.Models.Commands;

namespace library_api.Models.Mapper
{
    public class LibraryProfile : Profile
    {
        public LibraryProfile()
        {
            CreateMap<BookCommand, Book>()
            .DisableCtorValidation();

            CreateMap<AuthorCommand, Author>()
                .DisableCtorValidation();

            CreateMap<PublisherCommand, Publisher>()
                           .DisableCtorValidation();
        }
    }
}
