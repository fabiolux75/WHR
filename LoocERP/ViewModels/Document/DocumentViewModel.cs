using LoocERP.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace LoocERP.ViewModels
{
    public class DocumentViewModel
    {
        public DocumentViewModel()
        {
            DocumentTypeList= new List<Domain>();   
            redirectUrl = "DocumentIndex";  
            Document = new Document(); 
            area = "document_index";
            DocumentList= new List<Document>();  
        }
        /*
        public CreateDocumentViewModel(Data.ApplicationDBContext _context, string _area="document_index", String UserId = null)
        {                        
            area = _area;
            DocumentTypeList= new List<Domain>();  
            
            if(area.Equals("document_index")){ //document index 
                redirectRouteName = "DocumentIndex";
                DocumentTypeList = _context.Set<Domain>().Where( c => c.Tipo == "document_index" ).ToList();
                Document = new Document();
                Document.DocumentGroup = (DocumentGroup.DocumentArea==null?0:DocumentGroup.DocumentArea);
                Document.UserId = UserId;
            } 
            else if(area.Equals("document_profile")){ //document profile - area utente 
                redirectRouteName = "DocumentProfile";
                DocumentTypeList = _context.Set<Domain>().Where( c => c.Tipo == "document_profile" ).ToList();
                Document = new Document();
                Document.DocumentGroup = (DocumentGroup.ProfiloUtente==null?0:DocumentGroup.ProfiloUtente);
                Document.UserId = UserId;
            }      
                 
        }
        */
        
        public Document Document { get; set; }
        public List<Document> DocumentList { get; set; }
        public List<Domain> DocumentTypeList { get; set; }

        public string redirectUrl { get; set; }

        public string area { get; set; }

        //File per Upload
        [Display(Name = "File da caricare online...")]
        public IFormFile? FileUpload { get; set; }
    }
}
