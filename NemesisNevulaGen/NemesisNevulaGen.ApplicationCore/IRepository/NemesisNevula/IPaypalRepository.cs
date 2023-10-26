
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IPaypalRepository
{
void setSessionCP (GenericSessionCP session);

PaypalEN ReadOIDDefault (int id
                         );

void ModifyDefault (PaypalEN paypal);

System.Collections.Generic.IList<PaypalEN> ReadAllDefault (int first, int size);



int CrearPaypal (PaypalEN paypal);

void ModificarPaypal (PaypalEN paypal);


void BorrarPaypal (int id
                   );
}
}
