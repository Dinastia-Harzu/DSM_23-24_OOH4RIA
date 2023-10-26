
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula
{
public abstract class GenericUnitOfWorkRepository
{
protected IUsuarioRepository usuariorepository;
protected IUsuarioPremiumRepository usuariopremiumrepository;
protected IVisitanteRepository visitanterepository;
protected IAdministradorRepository administradorrepository;
protected IArticuloRepository articulorepository;
protected INoticiaRepository noticiarepository;
protected ICompraRepository comprarepository;
protected IMetodoPagoRepository metodopagorepository;
protected ITarjetaCreditoRepository tarjetacreditorepository;
protected IPaypalRepository paypalrepository;


public abstract IUsuarioRepository UsuarioRepository {
        get;
}
public abstract IUsuarioPremiumRepository UsuarioPremiumRepository {
        get;
}
public abstract IVisitanteRepository VisitanteRepository {
        get;
}
public abstract IAdministradorRepository AdministradorRepository {
        get;
}
public abstract IArticuloRepository ArticuloRepository {
        get;
}
public abstract INoticiaRepository NoticiaRepository {
        get;
}
public abstract ICompraRepository CompraRepository {
        get;
}
public abstract IMetodoPagoRepository MetodoPagoRepository {
        get;
}
public abstract ITarjetaCreditoRepository TarjetaCreditoRepository {
        get;
}
public abstract IPaypalRepository PaypalRepository {
        get;
}
}
}
