using profisysApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                return Ok(documents);
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Błąd: {exception.Message}");
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
                    return Ok("Brak dokumentów do usunięcia.");
                }

                await _documentsService.DeleteAllDocuments();

                return Ok("Dokumenty zostały pomyślnie usunięte.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Błąd: {exception.Message}");
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDocument(int documentId)
        {
            try
            {
                var document = await _documentsService.GetDocumentById(documentId);

                if (document == null)
                {
                    return NotFound("Nie znaleziono dokumentu do usunięcia.");
                }

                await _documentsService.DeleteDocument(document);
                

                return Ok("Dokument został pomyślnie usunięty.");
            }
            catch (Exception exception)
            {
                return StatusCode(500, $"Nieoczekiwny błąd: {exception.Message}");
            }
        }

    }
}
