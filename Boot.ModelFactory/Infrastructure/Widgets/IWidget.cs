using Boot.Multitenancy.Intrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boot.ModelFactory.Infrastructure.Widgets
{
    public interface IWidget : IEntity
    {
        Int32 Id { get; set; }
        Int32 PageId { get; set; }
        Region Zone { get; set; }
        string Template();
    }
}
