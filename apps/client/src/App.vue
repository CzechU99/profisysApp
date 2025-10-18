<template>

  <Toolbar />

  <div class="tableContainer">
    <DocumentsTable />
  </div>
</template>

<script>
import DocumentsTable from './components/DocumentsTable.vue';
import Toolbar from './components/Toolbar.vue';
import { useDocumentsStore } from './stores/documents'
import { onMounted } from 'vue'

export default {
  components: { DocumentsTable, Toolbar },
  setup() {
    const store = useDocumentsStore()
    
    onMounted(async () => {
      if(!await store.loadAllDocuments()){
        await store.loadDocsToDatabase()
      }
    })

    return { store }
  }
}
</script>

<style scoped> 

.tableContainer {
  width: 80%;
  margin: auto;
  margin-top: 50px;
}

</style>
