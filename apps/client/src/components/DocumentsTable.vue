<template>
  <DataTable 
    v-model:filters="filters"
    v-model:expandedRows="expandedRows"
    :value="documents" 
    dataKey="id" 
    :paginator="true" 
    :rows="10" 
    removableSort 
    :rowsPerPageOptions="[5, 10, 25, 100]"
    :globalFilterFields="['id', 'type', 'date', 'firstName', 'lastName', 'city']"
    filterDisplay="row"
    class="tableDocuments"
  >
    <template #header>
      <div class="flex justify-end">
        <IconField>
          <InputIcon class="pi pi-search" />
          <InputText v-model="filters.global.value" placeholder="Szukaj..." />
        </IconField>
      </div>
    </template>

    <Column expander />
    <Column field="id" sortable header="ID" />
    <Column field="type" sortable header="TYP" />
    <Column field="date" sortable header="DATA" />
    <Column field="firstName" sortable header="IMIĘ" />
    <Column field="lastName" sortable header="NAZWISKO" />
    <Column field="city" sortable header="MIASTO" />

    <template #expansion="slotProps">
      
      <DataTable 
        :value="slotProps.data.documentItem" 
        dataKey="id" 
        removableSort 
        class="subTable"
      >
        <Column field="product" sortable header="PRODUKT" />
        <Column field="quantity" sortable header="ILOŚĆ" />
        <Column field="taxRate" sortable header="PODATEK" />
        <Column field="price" sortable header="CENA" />
      </DataTable>
      
    </template>

  </DataTable>
</template>

<script>
import { ref, onMounted } from 'vue'
import { getAllDocuments } from '../api/documentsService'
import { FilterMatchMode } from '@primevue/core/api'

export default {
  setup() {
    const documents = ref([])
    const expandedRows = ref([])
    const filters = ref({
      global: { value: null, matchMode: FilterMatchMode.CONTAINS }
    })

    onMounted(async () => {
      documents.value = await getAllDocuments()
    })

    return { 
      documents, 
      filters, 
      expandedRows
    }
  }
}
</script>

<style scoped>
.tableDocuments {
  width: 90%;
  margin: auto;
}

.subTable {
  margin-left: 70px;
}

.tableDocuments :deep(thead) {
  background-color: #E7F7F3 !important; /* cyan-600 */
}

.tableDocuments :deep(th) {
  background-color: #E7F7F3 !important;
}

</style>
