import { defineStore } from 'pinia'
import { ref } from 'vue'
import { useToast } from 'vue-toastification'
import { getAllDocuments, clearAllDocuments, fetchAllDocuments, 
  deleteDocument, updateDocument } from '../api/documentsService'
import { deleteDocumentItem, updateItem } from '../api/itemsService'

export const useDocumentsStore = defineStore('documents', () => {
  const documents = ref([])
  const loading = ref(false)
  const deleting = ref(false)  
  const error = ref(null)
  const toast = useToast()

  function clearDocuments() {
    documents.value = []
    loading.value = false
    error.value = null
  }

  function handleApiError(error) {
    if (error.response?.data?.message) {
      toast.error(error.response.data.message);
    } else if (error.response?.status === 403) {
      toast.error("Nie masz uprawnień do tej operacji!"); 
    } else if (error.response?.status === 401) {
      toast.error("Musisz się zalogować!"); 
    } else if (error.message) {
      toast.error("Błąd połączenia z serwerem!"); 
    } else {
      toast.error("Nieznany błąd");
    }
  }

  async function loadAllDocuments() {
    try {
      loading.value = true
      const response = await getAllDocuments()
      documents.value = response.data.documents
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      loading.value = false
    }
  }

  async function deleteAllDocuments() {
    deleting.value = true
    try {
      const response = await clearAllDocuments()
      this.clearDocuments()
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    } finally {
      deleting.value = false
    }
  }

  async function loadDocsToDatabase() {
    loading.value = true

    try {
      const responseCsvFiles = await fetchAllDocuments()
      toast.info(responseCsvFiles.data.message)
    } catch (error) {
      handleApiError(error)
    }

    try {
      const responseServer = await getAllDocuments()
      documents.value = responseServer.data.documents
      toast.info(responseServer.data.message)
    } catch (error) {
      handleApiError(error)
    }

    loading.value = false
  }

  async function deleteDocumentById(id) {
    try {
      const response = await deleteDocument(id)
      documents.value = documents.value.filter(doc => doc.id !== id)
      toast.info(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function updateDocuments(document) {
    try {
      const response = await updateDocument(document)
      const index = documents.value.findIndex(d => d.id === document.id)
      if (index !== -1) documents.value[index] = { ...document }
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function updateItems(item) {
    try {
      const response = await updateItem(item);
      
      const document = documents.value.find(d => 
        d.documentItem?.some(i => i.id === item.id)
      );
      
      if (document) {
        const itemIndex = document.documentItem.findIndex(i => i.id === item.id);
        if (itemIndex !== -1) {
          document.documentItem[itemIndex] = { ...item };
        }
      }
      
      toast.success(response.data.message);
    } catch (error) {
      console.error('Błąd updateItems:', error);
      handleApiError(error);
    }
  }

  async function deleteDocumentItemById(itemId) {
    try {
      const response = await deleteDocumentItem(itemId)

      const document = documents.value.find(d =>
        d.documentItem.some(i => i.id === itemId)
      )

      if (document) {
        document.documentItem = document.documentItem.filter(i => i.id !== itemId)
      }

      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  return {
    documents,
    loading,
    deleting,
    error,
    loadAllDocuments,
    deleteAllDocuments,
    loadDocsToDatabase,
    deleteDocumentById,
    clearDocuments,
    updateDocuments,
    deleteDocumentItemById,
    updateItems
  }
})
