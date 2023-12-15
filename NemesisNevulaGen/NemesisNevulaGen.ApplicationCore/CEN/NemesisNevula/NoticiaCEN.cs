

using System;
using System.Text;
using System.Collections.Generic;

using NemesisNevulaGen.ApplicationCore.Exceptions;

using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;


namespace NemesisNevulaGen.ApplicationCore.CEN.NemesisNevula
{
/*
 *      Definition of the class NoticiaCEN
 *
 */
public partial class NoticiaCEN
{
private INoticiaRepository _INoticiaRepository;

public NoticiaCEN(INoticiaRepository _INoticiaRepository)
{
        this._INoticiaRepository = _INoticiaRepository;
}

public INoticiaRepository get_INoticiaRepository ()
{
        return this._INoticiaRepository;
}

public int CrearNoticia (string p_descripcion, bool p_esPublicada, string p_titulo, string p_foto)
{
        NoticiaEN noticiaEN = null;
        int oid;

        //Initialized NoticiaEN
        noticiaEN = new NoticiaEN ();
        noticiaEN.Descripcion = p_descripcion;

        noticiaEN.EsPublicada = p_esPublicada;

        noticiaEN.Titulo = p_titulo;

        noticiaEN.Foto = p_foto;



        oid = _INoticiaRepository.CrearNoticia (noticiaEN);
        return oid;
}

public void ModificarNoticia (int p_Noticia_OID, string p_descripcion, bool p_esPublicada, string p_titulo, string p_foto)
{
        NoticiaEN noticiaEN = null;

        //Initialized NoticiaEN
        noticiaEN = new NoticiaEN ();
        noticiaEN.Id = p_Noticia_OID;
        noticiaEN.Descripcion = p_descripcion;
        noticiaEN.EsPublicada = p_esPublicada;
        noticiaEN.Titulo = p_titulo;
        noticiaEN.Foto = p_foto;
        //Call to NoticiaRepository

        _INoticiaRepository.ModificarNoticia (noticiaEN);
}

public void BorrarNoticia (int id
                           )
{
        _INoticiaRepository.BorrarNoticia (id);
}

public NoticiaEN DamePorOID (int id
                             )
{
        NoticiaEN noticiaEN = null;

        noticiaEN = _INoticiaRepository.DamePorOID (id);
        return noticiaEN;
}

public System.Collections.Generic.IList<NoticiaEN> DameTodos (int first, int size)
{
        System.Collections.Generic.IList<NoticiaEN> list = null;

        list = _INoticiaRepository.DameTodos (first, size);
        return list;
}
}
}
