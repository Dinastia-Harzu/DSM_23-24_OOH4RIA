

using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;
using NemesisNevulaGen.Infraestructure.Repository.NemesisNevula;
using NemesisNevulaGen.Infraestructure.CP;
using System;
using System.Collections.Generic;
using System.Text;

namespace NemesisNevulaGen.Infraestructure.Repository
{
public class UnitOfWorkRepository : GenericUnitOfWorkRepository
{
SessionCPNHibernate session;


public UnitOfWorkRepository(SessionCPNHibernate session)
{
        this.session = session;
}

public override IUsuarioRepository UsuarioRepository {
        get
        {
                this.usuariorepository = new UsuarioRepository ();
                this.usuariorepository.setSessionCP (session);
                return this.usuariorepository;
        }
}

public override IUsuarioPremiumRepository UsuarioPremiumRepository {
        get
        {
                this.usuariopremiumrepository = new UsuarioPremiumRepository ();
                this.usuariopremiumrepository.setSessionCP (session);
                return this.usuariopremiumrepository;
        }
}

public override IAdministradorRepository AdministradorRepository {
        get
        {
                this.administradorrepository = new AdministradorRepository ();
                this.administradorrepository.setSessionCP (session);
                return this.administradorrepository;
        }
}

public override IArticuloRepository ArticuloRepository {
        get
        {
                this.articulorepository = new ArticuloRepository ();
                this.articulorepository.setSessionCP (session);
                return this.articulorepository;
        }
}

public override ICompraRepository CompraRepository {
        get
        {
                this.comprarepository = new CompraRepository ();
                this.comprarepository.setSessionCP (session);
                return this.comprarepository;
        }
}

public override IMetodoPagoRepository MetodoPagoRepository {
        get
        {
                this.metodopagorepository = new MetodoPagoRepository ();
                this.metodopagorepository.setSessionCP (session);
                return this.metodopagorepository;
        }
}

public override ITarjetaCreditoRepository TarjetaCreditoRepository {
        get
        {
                this.tarjetacreditorepository = new TarjetaCreditoRepository ();
                this.tarjetacreditorepository.setSessionCP (session);
                return this.tarjetacreditorepository;
        }
}

public override IPaypalRepository PaypalRepository {
        get
        {
                this.paypalrepository = new PaypalRepository ();
                this.paypalrepository.setSessionCP (session);
                return this.paypalrepository;
        }
}

public override IValoracionArticuloRepository ValoracionArticuloRepository {
        get
        {
                this.valoracionarticulorepository = new ValoracionArticuloRepository ();
                this.valoracionarticulorepository.setSessionCP (session);
                return this.valoracionarticulorepository;
        }
}

public override INoticiaRepository NoticiaRepository {
        get
        {
                this.noticiarepository = new NoticiaRepository ();
                this.noticiarepository.setSessionCP (session);
                return this.noticiarepository;
        }
}
}
}

