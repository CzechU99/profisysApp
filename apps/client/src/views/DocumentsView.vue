<template>

  <Toolbar />

  <div class="tableContainer">
    <DocumentsTable />
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