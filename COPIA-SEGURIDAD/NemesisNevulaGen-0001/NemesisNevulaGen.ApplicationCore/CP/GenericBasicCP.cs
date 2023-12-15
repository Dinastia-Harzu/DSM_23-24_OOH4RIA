

using System;
using System.Collections.Generic;
using NemesisNevulaGen.ApplicationCore.IRepository.NemesisNevula;

namespace NemesisNevulaGen.ApplicationCore.CP.NemesisNevula
{
public abstract class GenericBasicCP
{
protected GenericSessionCP CPSession;
protected GenericUnitOfWorkRepository unitRepo;

protected GenericBasicCP (GenericSessionCP currentSession)
{
        this.CPSession = currentSession;
        this.unitRepo = this.CPSession.UnitRepo;
}
protected GenericBasicCP()
{
        this.CPSession = null;
        this.unitRepo = null;
}
}
}

