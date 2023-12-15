
using System;
// Definición clase NoticiaEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class NoticiaEN
{
/**
 *	Atributo id
 */
private int id;



/**
 *	Atributo titulo
 */
private string titulo;



/**
 *	Atributo foto
 */
private string foto;



/**
 *	Atributo descripcion
 */
private string descripcion;



/**
 *	Atributo esPublicada
 */
private bool esPublicada;



/**
 *	Atributo administradorGestiona
 */
private System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> administradorGestiona;






public virtual int Id {
        get { return id; } set { id = value;  }
}



public virtual string Titulo {
        get { return titulo; } set { titulo = value;  }
}



public virtual string Foto {
        get { return foto; } set { foto = value;  }
}



public virtual string Descripcion {
        get { return descripcion; } set { descripcion = value;  }
}



public virtual bool EsPublicada {
        get { return esPublicada; } set { esPublicada = value;  }
}



public virtual System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> AdministradorGestiona {
        get { return administradorGestiona; } set { administradorGestiona = value;  }
}





public NoticiaEN()
{
        administradorGestiona = new System.Collections.Generic.List<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN>();
}



public NoticiaEN(int id, string titulo, string foto, string descripcion, bool esPublicada, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> administradorGestiona
                 )
{
        this.init (Id, titulo, foto, descripcion, esPublicada, administradorGestiona);
}


public NoticiaEN(NoticiaEN noticia)
{
        this.init (noticia.Id, noticia.Titulo, noticia.Foto, noticia.Descripcion, noticia.EsPublicada, noticia.AdministradorGestiona);
}

private void init (int id
                   , string titulo, string foto, string descripcion, bool esPublicada, System.Collections.Generic.IList<NemesisNevulaGen.ApplicationCore.EN.NemesisNevula.AdministradorEN> administradorGestiona)
{
        this.Id = id;


        this.Titulo = titulo;

        this.Foto = foto;

        this.Descripcion = descripcion;

        this.EsPublicada = esPublicada;

        this.AdministradorGestiona = administradorGestiona;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        NoticiaEN t = obj as NoticiaEN;
        if (t == null)
                return false;
        if (Id.Equals (t.Id))
                return true;
        else
                return false;
}

public override int GetHashCode ()
{
        int hash = 13;

        hash += this.Id.GetHashCode ();
        return hash;
}
}
}
