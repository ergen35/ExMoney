using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExMoney.SharedLibs
{
    public class KycVerification
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required] public string UserId { get; set; }

        [EnumDataType(typeof(IdDocumentType))]
        public IdDocumentType DocumentType { get; set; } = IdDocumentType.IdCard;

        public byte[] IdDocumentRectoPic { get; set; }
        public byte[] IdDocumentVersoPic { get; set; }
        public byte[] UserPic { get; set; }

        [Required]
        public KycVerificationResult VerificationResult { get; set; } = KycVerificationResult.NoStatus;
    }
}
