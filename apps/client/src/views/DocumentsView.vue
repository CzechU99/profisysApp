<template>

  <Toolbar />

  <div class="tableContainer">
    <ProgressSpinner v-if="documentsStore.loading || documentsStore.deleting" class="my-spinner" />
    <DocumentsTable v-else />
  </div>
  
</template>

<script>
import { onMounted } from 'vue';
import DocumentsTable from '../components/documents/table/DocumentsTable.vue';
import Toolbar from '../components/toolbar/Toolbar.vue';
import { useDocumentsStore } from '../stores/documentsStore'

export default {
  components: { DocumentsTable, Toolbar },
  setup() {
    const documentsStore = useDocumentsStore()

    onMounted(async () => {
      await documentsStore.loadAllDocuments()
    })

    return { documentsStore }
  }
}
</script>

<style scoped>
.my-spinner{
  display:flex; 
  justify-content:center; 
  align-items:center; 
  height:300px;
}
</style>