

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class VisitanteCEN
 *
 */
public partial class VisitanteCEN
{
private IVisitanteRepository _IVisitanteRepository;

public VisitanteCEN(IVisitanteRepository _IVisitanteRepository)
{
        this._IVisitanteRepository = _IVisitanteRepository;
}

public IVisitanteRepository get_IVisitanteRepository ()
{
        return this._IVisitanteRepository;
}

public int CrearVisitante ()
{
        VisitanteEN visitanteEN = null;
        int oid;

        //Initialized VisitanteEN
        visitanteEN = new VisitanteEN ();


        oid = _IVisitanteRepository.CrearVisitante (visitanteEN);
        return oid;
}

public void ModificarVisitante (int p_Visitante_OID)
{
        VisitanteEN visitanteEN = null;

        //Initialized VisitanteEN
        visitanteEN = new VisitanteEN ();
        visitanteEN.Id = p_Visitante_OID;
        //Call to VisitanteRepository

        _IVisitanteRepository.ModificarVisitante (visitanteEN);
}

public void BorrarVisitante (int id
                             )
{
        _IVisitanteRepository.BorrarVisitante (id);
}

public VisitanteEN DamePorOID (int id
                               )
{
        VisitanteEN visitanteEN = null;

        visitanteEN = _IVisitanteRepository.DamePorOID (id);
        return visitanteEN;
}

public System.Collections.Generic.IList<VisitanteEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<VisitanteEN> list = null;

        list = _IVisitanteRepository.DameTodos (first, size);
        return list;
}
}
}
