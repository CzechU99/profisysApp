<template>

  <Toolbar />

  <div class="tableContainer">
    <DocumentsTable />
  </div>
  
</template>

<script>
import DocumentsTable from '../components/DocumentsTable.vue';
import Toolbar from '../components/Toolbar.vue';
import { useDocumentsStore } from '../stores/documents'
import { onMounted } from 'vue'

export default {
  components: { DocumentsTable, Toolbar },
  setup() {
    const store = useDocumentsStore()
    
    onMounted(async () => {
      await store.loadAllDocuments()

      if (store.documents.length === 0) {
        await store.loadDocsToDatabase()
      }
    })

    return { store }
  }
}
</script>