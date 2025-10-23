<template>

  <Toolbar />

  <div class="tableContainer">
    <ProgressSpinner v-if="store.loading || store.deleting" class="my-spinner" />
    <DocumentsTable v-else />
  </div>
  
</template>

<script>
import { onMounted } from 'vue';
import DocumentsTable from '../components/DocumentsTable.vue';
import Toolbar from '../components/Toolbar.vue';
import { useDocumentsStore } from '../stores/documentsStore'

export default {
  components: { DocumentsTable, Toolbar },
  setup() {
    const store = useDocumentsStore()

    onMounted(async () => {
      await store.loadAllDocuments()
    })

    return { store }
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