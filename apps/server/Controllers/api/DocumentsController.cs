using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profisysApp.Services;
using System.Text.Json;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsService _documentsService;
        private readonly AuditService _auditService;

        public DocumentsController(DocumentsService documentsService, AuditService auditService)
        {
            _documentsService = documentsService;
            _auditService = auditService;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> GetAllDocuments()
        {
            try
            {
                var documents = await _documentsService.GetAllDocumentsAsync();
                if (documents == null || !documents.Any())
                {
                    return Ok(new { message = "Administrator nie dodał żadnych dokumentów!"});
                }

                await _auditService.LogAsync(User, "Wczytanie dokumentów z bazy");
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
                var documents = await _documentsService.GetAllDocumentsAsync();

                if (documents == null || !documents.Any())
                {
                    return Ok(new { message = "Brak dokumentów do usunięcia!" });
                }

                await _documentsService.DeleteAllDocumentsAsync();
                await _auditService.LogAsync(User, "Usunięcie wszystkich dokumentów z bazy");

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
                var document = await _documentsService.GetDocumentByIdAsync(documentId);

                if (document == null)
                {
                    return NotFound(new { message = "Nie znaleziono takiego dokumentu!" });
                }

                await _documentsService.DeleteDocumentAsync(document);
                await _auditService.LogAsync(User, "Usunięcie dokumentu", $"DocumentId: {documentId}");

                return Ok(new { message = "Dokument został pomyślnie usunięty!" });
            }
            catch
            {
                return StatusCode(500, new { message = "Błąd podczas usuwania dokumentu!" });
            }
        }
        
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDocument([FromBody] JsonElement updatedDocument)
        {
            try
            {
                var serializedDocument = _documentsService.SerializeDocument(updatedDocument);
        
                if (serializedDocument == null)
                    return BadRequest(new { message = "Nie można zdekodować danych dokumentu." });

                var documentToUpdate = await _documentsService.GetDocumentByIdAsync(serializedDocument.Id);

                if (documentToUpdate == null)
                    return NotFound(new { message = "Nie znaleziono dokumentu." });

                await _documentsService.UpdateDocumentAsync(serializedDocument, documentToUpdate);
                await _auditService.LogAsync(User, "Aktualizacja dokumentu", $"DocumentId: {documentToUpdate.Id}");

                return Ok(new { message = "Dokument zaktualizowany pomyślnie!" });
            }
            catch
            {
                return StatusCode(500, new { message = "Błąd podczas edytowania dokumentu!" });
            }
            
        }

    }
}
