
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface INoticiaRepository
{
void setSessionCP (GenericSessionCP session);

NoticiaEN ReadOIDDefault (int id
                          );

void ModifyDefault (NoticiaEN noticia);

System.Collections.Generic.IList<NoticiaEN> ReadAllDefault (int first, int size);



int CrearNoticia (NoticiaEN noticia);

void ModificarNoticia (NoticiaEN noticia);


void BorrarNoticia (int id
                    );


NoticiaEN DamePorOID (int id
                      );


System.Collections.Generic.IList<NoticiaEN> DameTodos (int first, int size);
}
}
