<template>
  <div class="expansion-wrapper">
    <DataTable 
      :value="items" 
      dataKey="id" 
      removableSort 
      class="subTable"
    >
      <Column field="product" sortable header="PRODUKT" />
      <Column field="quantity" sortable header="ILOŚĆ" />
      <Column field="taxRate" sortable header="PODATEK" />
      <Column field="price" sortable header="CENA" />
    </DataTable>
  </div>
</template>

<script setup>
defineProps({
  items: {
    type: Array,
    default: () => []
  }
})
</script>

<style scoped>
.expansion-wrapper {
  padding: 1rem;
}
</style>