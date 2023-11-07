
using System;
// Definición clase VisitanteEN
namespace NemesisNevulaGen.ApplicationCore.EN.NemesisNevula
{
public partial class VisitanteEN
{
/**
 *	Atributo id
 */
private int id;






public virtual int Id {
        get { return id; } set { id = value;  }
}





public VisitanteEN()
{
}



public VisitanteEN(int id
                   )
{
        this.init (Id);
}


public VisitanteEN(VisitanteEN visitante)
{
        this.init (visitante.Id);
}

private void init (int id
                   )
{
        this.Id = id;
}

public override bool Equals (object obj)
{
        if (obj == null)
                return false;
        VisitanteEN t = obj as VisitanteEN;
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
