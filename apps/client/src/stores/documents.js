import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { getAllDocuments, clearAllDocuments, fetchAllDocuments, deleteDocument } from '../api/documentsService'

export const useDocumentsStore = defineStore('documents', () => {
  const documents = ref([])
  const loading = ref(false)
  const error = ref(null)
  const toast = useToast()

  async function loadAllDocuments() {
    try {
      loading.value = true
      const response = await getAllDocuments()
      documents.value = response
      toast.info('Dokumenty wczytane pomyślnie!')
    } catch (responseError) {
      toast.error('Nie udało się pobrać dokumentów z bazy danych.')
    } finally {
      loading.value = false
    }
  }

  async function deleteAllDocuments() {
    try {
      await clearAllDocuments()
      documents.value = [] 
      toast.info('Usunięto wszystkie dokumenty!')
    } catch (responseError) {
      toast.error('Brak dokumentów do usunięcia.')
    }
  }

  async function loadDocsToDatabase() {
    try {
      loading.value = true
      await fetchAllDocuments()
      const response = await getAllDocuments()
      documents.value = response
      toast.info('Dokumenty wczytane pomyślnie!')
    } catch (responseError) {
      toast.error('Nie udało się wczytać dokumentów do bazy danych.')
    } finally {
      loading.value = false
    }
  }

  async function deleteDocumentById(id) {
    try {
      await deleteDocument(id)
      documents.value = documents.value.filter(doc => doc.id !== id)
      toast.info('Dokument usunięto pomyślnie!')
    } catch (responseError) {
      toast.error('Nie udało się usunąć dokumentu.')
    }
  }

  return {
    documents,
    loading,
    error,
    loadAllDocuments,
    deleteAllDocuments,
    loadDocsToDatabase,
    deleteDocumentById
  }
})
