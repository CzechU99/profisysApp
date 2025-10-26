<template>
  <div class="expansion-wrapper">
    <DataTable 
      :value="items" 
      dataKey="id" 
      removableSort 
      class="subTable"
      contextMenu
      v-model:contextMenuSelection="selectedItem"
      @row-contextmenu="onRowContextMenu"
    >
      <Column field="product" sortable header="PRODUKT" />
      <Column field="quantity" sortable header="ILOŚĆ" />
      <Column field="taxRate" sortable header="PODATEK" />
      <Column field="price" sortable header="CENA" />
    </DataTable>
  </div>
</template>

<script setup>
import { ref } from 'vue'

const selectedItem = ref(null)
const emit = defineEmits(['item-context-menu'])

const props = defineProps({
  items: {
    type: Array,
    default: () => []
  }
})

const onRowContextMenu = (event) => {
  emit('item-context-menu', event)
}
</script>

<style scoped>
.expansion-wrapper {
  padding: 1rem;
}
</style>
