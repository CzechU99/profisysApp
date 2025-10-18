<template>

  <Toolbar />

  <div class="tableContainer">
    <DocumentsTable />
  </div>
</template>

<script>
import DocumentsTable from './components/DocumentsTable.vue';
import Toolbar from './components/Toolbar.vue';

export default {
  components: { DocumentsTable, Toolbar }
};
</script>

<style scoped> 

.tableContainer {
  width: 80%;
  margin: auto;
  margin-top: 50px;
}

</style>
