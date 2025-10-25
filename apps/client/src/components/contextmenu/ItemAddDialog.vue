<template>
  <Dialog 
    :visible="visible"
    @update:visible="$emit('update:visible', $event)"
    header="Dodaj item"
    :modal="true" 
    class="add-dialog-menu"
    style="width: 1220px"
  >
    <div v-if="item">
      <div class="p-fluid formgrid grid container">
        <div class="field col-6">
          <label for="product">Produkt:</label>
          <InputText 
            inputId="product" 
            v-model="item.product"
          />
        </div>
        <div class="field col-6">
          <label for="quantity">Ilość:</label>
          <InputNumber 
            inputId="quantity" 
            v-model="item.quantity"
            :useGrouping="false"
          />
        </div>
        <div class="field col-6">
          <label for="taxRate">Podatek:</label>
          <InputNumber 
            inputId="taxRate" 
            v-model="item.taxRate"
            suffix="%"
            :useGrouping="false"
          />
        </div>
        <div class="field col-6">
          <label for="price">Cena:</label>
          <InputNumber
            inputId="price" 
            v-model="item.price"
            mode="currency" 
            currency="PLN" 
            locale="pl-PL"
          />
        </div>
      </div>
    </div>

    <template #footer>
      <Button 
        label="Zapisz" 
        icon="pi pi-check" 
        class="p-button-success" 
        @click="handleSave" 
      />
    </template>
  </Dialog>
</template>

<script setup>
import { ref, watch } from 'vue'

const props = defineProps({
  visible: Boolean,
  item: Object
})

const emit = defineEmits(['update:visible', 'update:item', 'save'])

const item = ref({
  documentId: null,
  product: '',
  quantity: 1,
  price: 0.01,
  taxRate: 1,
  ordinal: 1
})

watch(() => props.item, (newVal) => {
  if (newVal) {
    item.value = {
      documentId: newVal.documentId || null,
      product: newVal.product || '',
      quantity: newVal.quantity || 1,
      price: newVal.price || 0.01,
      taxRate: newVal.taxRate || 1,
      ordinal: newVal.ordinal || 1
    }
  }
}, { immediate: true, deep: true })

const handleSave = () => {
  emit('update:item', item.value)
  emit('save')
  emit('update:visible', false)
}
</script>

<style scoped>
.field {
  float: left;
  margin-left: 40px;  
}

.field label {
  display: block;
  margin-bottom: 0.5rem;
}

.p-error {
  color: #e24c4c;
  font-size: 0.875rem;
  margin-top: 0.25rem;
  display: block;
}
</style>