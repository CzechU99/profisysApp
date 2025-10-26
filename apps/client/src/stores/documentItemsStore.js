import { defineStore } from 'pinia'
import { useToast } from 'vue-toastification'
import { 
  create as apiCreate, 
  update as apiUpdate, 
  remove as apiRemove 
} from '../api/itemsService'
import { handleApiError } from '../utils/errorHandler'
import { useDocumentsStore } from './documentsStore'

export const useDocumentItemsStore = defineStore('documentItems', () => {
  const toast = useToast()

  async function update(item) {
    try {
      const response = await apiUpdate(item)
      const documentsStore = useDocumentsStore()
      
      const document = documentsStore.documents.find(d => 
        d.documentItem?.some(i => i.id === item.id)
      )
      
      if (document) {
        const itemIndex = document.documentItem.findIndex(i => i.id === item.id)
        if (itemIndex !== -1) {
          document.documentItem[itemIndex] = { ...item }
        }
      }
      
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function remove(itemId) {
    try {
      const response = await apiRemove(itemId)
      const documentsStore = useDocumentsStore()

      const document = documentsStore.documents.find(d =>
        d.documentItem?.some(i => i.id === itemId)
      )

      if (document) {
        document.documentItem = document.documentItem.filter(i => i.id !== itemId)
      }

      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  async function create(item) {
    try {
      const response = await apiCreate(item)
      const documentsStore = useDocumentsStore()
      
      item.id = response.data.itemId

      const document = documentsStore.documents.find(d => d.id === item.documentId)
      if (document) {
        if (!document.documentItem) {
          document.documentItem = []
        }
        document.documentItem.push(item)
      }
      
      toast.success(response.data.message)
    } catch (error) {
      handleApiError(error)
    }
  }

  return {
    update,
    remove,
    create
  }
})