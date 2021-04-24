using System.Collections.Generic;
using System.Linq;

namespace Study.PaymentGateway.Domain.Entities.Bases
{
    public abstract class Validation
    {
        public IReadOnlyList<string> ErrorMessages => GetErrorMessages();

        public virtual bool IsValid()
        {
            return !GetErrorMessages().Any();
        }

        public abstract IReadOnlyList<string> GetErrorMessages();
    }
}