using library_api.Models.Enums;
using System;

namespace library_api.Models
{   

    public class Book : Entity
    {
        protected Book() { }
        public Book(string title, DateTime publicationDate, Guid authorId, Guid publisherId, string iSNB, Language language)
        {
            Title = title;
            PublicationDate = publicationDate;
            AuthorId = authorId;
            PublisherId = publisherId;
            ISNB = iSNB;
            Language = language;
        }

        public string Title { get; set; }
        public Guid AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public Guid PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISNB { get; set; }
        public Language Language { get; set; }
    }
}