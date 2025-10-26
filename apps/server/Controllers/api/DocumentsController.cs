using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using profisysApp.Services;
using profisysApp.Models;
using profisysApp.Entities;
using AutoMapper;

namespace profisysApp.Controllers
{
    [ApiController]
    [Route("api/documents")]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentsService _documentsService;
        private readonly AuditService _auditService;
        private readonly IMapper _mapper;

        public DocumentsController(
            IMapper mapper,
            DocumentsService documentsService,
            AuditService auditService
        )
        {
            _documentsService = documentsService;
            _auditService = auditService;
            _mapper = mapper;
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
                    return Ok(new { 
                        documents = new List<Documents>(), 
                        message = "Administrator nie dodał żadnych dokumentów!" 
                    });
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
        public async Task<IActionResult> UpdateDocument([FromBody] DocumentDto updatedDocument)
        {
            try
            {
                var documentToUpdate = await _documentsService.GetDocumentByIdAsync(updatedDocument.Id);

                if (documentToUpdate == null)
                    return NotFound(new { message = "Nie znaleziono dokumentu." });

                await _documentsService.UpdateDocumentAsync(updatedDocument, documentToUpdate);
                await _auditService.LogAsync(User, "Aktualizacja dokumentu", $"DocumentId: {documentToUpdate.Id}");

                return Ok(new { message = "Dokument zaktualizowany pomyślnie!" });
            }
            catch
            {
                return StatusCode(500, new { message = "Błąd podczas edytowania dokumentu!" });
            }

        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddDocument([FromBody] DocumentDto document)
        {
            try
            {
                var newDocument = _mapper.Map<Documents>(document);

                await _documentsService.AddDocumentAsync(newDocument);
                await _documentsService.SaveChangesDocumentsAsync();

                await _auditService.LogAsync(User, "Dodanie dokumentu", $"DokumentId: {newDocument.Id}");

                return Ok(new
                {
                    message = "Dodano nowy dokument!",
                    documentId = newDocument.Id
                });
            }
            catch
            {
                return StatusCode(500, new { message = "Błąd podczas dodawania dokumentu!" });
            }
        }

    }
}
