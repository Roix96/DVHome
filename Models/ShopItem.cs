using System.ComponentModel.DataAnnotations;

namespace DVHome.Models
{
    public class ShopItem
    {
        [Key]
        public string Item {get; set;}
        public int Count {get; set;} = 0;
        public string? AddedBy {get; set;}
        public long? AddedAt {get; set;}
        public string? LastModifiedBy {get; set;}
        public long? ModifiedAt {get; set;}
    }

    

}