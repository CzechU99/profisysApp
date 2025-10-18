import { defineStore } from 'pinia'
import { ref } from 'vue'
import { getAllDocuments, clearAllDocuments, fetchAllDocuments, deleteDocument } from '../api/documentsService'

export const useDocumentsStore = defineStore('documents', () => {
  const documents = ref([])
  const loading = ref(false)
  const error = ref(null)

  async function loadAllDocuments() {
    try {
      loading.value = true
      const response = await getAllDocuments()
      documents.value = response
    } catch (err) {
      console.error('Błąd wczytywania dokumentów:', err)
      error.value = err
    } finally {
      loading.value = false
    }
  }

  async function deleteAllDocuments() {
    try {
      await clearAllDocuments()
      documents.value = [] 
    } catch (err) {
      console.error('Błąd usuwania dokumentów:', err)
      error.value = err
    }
  }

  async function loadDocsToDatabase() {
    try {
      loading.value = true
      await fetchAllDocuments()
      const response = await getAllDocuments()
      documents.value = response
    } catch (err) {
      console.error('Błąd wczytywania dokumentów:', err)
      error.value = err
    } finally {
      loading.value = false
    }
  }

  async function deleteDocumentById(id) {
    try {
      await deleteDocument(id)
      documents.value = documents.value.filter(doc => doc.id !== id)
    } catch (err) {
      console.error('Błąd usuwania dokumentów:', err)
      error.value = err
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
