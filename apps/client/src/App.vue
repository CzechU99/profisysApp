<template>
  <div class="tableContainer">
    <DocumentsTable />
  </div>
</template>

<script>
import DocumentsTable from './components/DocumentsTable.vue';

export default {
  components: { DocumentsTable }
};
</script>

<style scoped> 

.tableContainer {
  width: 80%;
  margin: auto;
  margin-top: 50px;
}

</style>
