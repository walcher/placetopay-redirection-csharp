using System.Collections.Generic;

namespace PlacetoPay.Redirection.Validators
{
    interface IValidator
    {
        bool IsValid(object entity, List<string> fields, bool silent);
    }
}
