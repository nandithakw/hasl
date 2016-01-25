using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xcendant.HASL.Entities
{
    public class ProfileImage
    {

        [Key, ForeignKey("RegisteredUser")]
        public int RegisteredUserId { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public virtual RegisteredUser RegisteredUser { get; set; }

    }
}
