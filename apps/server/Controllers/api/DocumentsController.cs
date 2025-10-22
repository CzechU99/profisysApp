using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profisysApp.Services;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsService _documentsService;

        public DocumentsController(DocumentsService documentsService)
        {
            _documentsService = documentsService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                var documents = await _documentsService.GetAllDocuments();
                if (documents == null || !documents.Any())
                {
                    return Ok(new { message = "Administrator nie dodał żadnych dokumentów!"});
                }

                return Ok(new { documents, message = "Dokumenty wczytane pomyślnie!" });
            }
            catch 
            {
                return StatusCode(500, new { message = "Wystąpił błąd podczas pobierania dokumentów!" });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAllDocuments()
        {
            try
            {
                var documents = await _documentsService.GetAllDocuments();

                if (documents == null || !documents.Any())
                {
                    return Ok(new { message = "Brak dokumentów do usunięcia!" });
                }

                await _documentsService.DeleteAllDocuments();

                return Ok(new { message = "Dokumenty zostały pomyślnie usunięte!" });
            }
            catch 
            {
                return StatusCode(500, new { message = "Błąd podczas usuwania dokumentów!" });
            }
        }

        [HttpDelete("{documentId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                var document = await _documentsService.GetDocumentById(documentId);

                if (document == null)
                {
                    return NotFound(new { message = "Nie znaleziono takiego dokumentu!" });
                }

                await _documentsService.DeleteDocument(document);

                return Ok(new { message = "Dokument został pomyślnie usunięty!" });
            }
            catch 
            {
                return StatusCode(500, new { message = "Błąd podczas usuwania dokumentu!" });
            }
        }

    }
}
