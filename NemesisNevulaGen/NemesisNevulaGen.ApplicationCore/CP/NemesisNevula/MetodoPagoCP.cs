
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula;



namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public partial class MetodoPagoCP : GenericBasicCP
{
public MetodoPagoCP(GenericSessionCP currentSession)
        : base (currentSession)
{
}
}
}
