
using System;
using System.Text;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.Exceptions;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


/*PROTECTED REGION ID(usingNemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Noticia_publicarNoticia) ENABLED START*/
//  references to other libraries
/*PROTECTED REGION END*/

namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
public partial class NoticiaCEN
{
public void PublicarNoticia (int p_oid)
{
        /*PROTECTED REGION ID(NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula_Noticia_publicarNoticia) ENABLED START*/

        NoticiaEN noticia = _INoticiaRepository.DamePorOID (p_oid);

        noticia.EsPublicada = true;
        Console.WriteLine ("\n\nNoticia publicada: " + noticia.EsPublicada);

        _INoticiaRepository.ModificarNoticia (noticia);

        /*PROTECTED REGION END*/
}
}
}
