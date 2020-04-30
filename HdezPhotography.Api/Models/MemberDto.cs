using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HdezPhotography.Api.Models {
    public class MemberDto : IValidatableObject {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
            if (!Email.Contains(Name)) {
                yield return new ValidationResult(
                    "The provided email does NOT contain the name of the member... weird, but wrong :)",
                    new[] { "MemberDto" });
            }
        }
    }

    public class MemberImportDto {
        public string Name { get; set; }
        public string PhoneNum { get; set; }
        public string Email { get; set; }
        public ICollection<PhotoImportDto> Photos { get; set; }
            = new List<PhotoImportDto>();
    }
}
