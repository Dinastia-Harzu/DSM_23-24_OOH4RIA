
using System;
using NemesisNevulaGen.ApplicationCore.EN.NemesisNevula;
using NemesisNevulaGen.ApplicationCore.CP.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public partial interface IVisitanteRepository
{
void setSessionCP (GenericSessionCP session);

VisitanteEN ReadOIDDefault (int id
                            );

void ModifyDefault (VisitanteEN visitante);

System.Collections.Generic.IList<VisitanteEN> ReadAllDefault (int first, int size);



int CrearVisitante (VisitanteEN visitante);

void ModificarVisitante (VisitanteEN visitante);


void BorrarVisitante (int id
                      );




VisitanteEN DamePorOID (int id
                        );


System.Collections.Generic.IList<VisitanteEN> DameTodos (int first, int size);
}
}
