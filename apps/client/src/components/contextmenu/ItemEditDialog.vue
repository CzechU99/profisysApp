<template>
  <Dialog 
    :visible="visible"
    @update:visible="$emit('update:visible', $event)"
    header="Edytuj item"
    :modal="true" 
    class="edit-dialog-menu"
    style="width: 1220px"
  >
    <div v-if="item">
      <div class="p-fluid formgrid grid container">
        <div class="field col-6">
          <label for="product">Produkt:</label>
          <InputText 
            inputId="product" 
            :modelValue="item.product"
            @update:modelValue="updateField('product', $event)"
          />
        </div>
        <div class="field col-6">
          <label for="quantity">Ilość:</label>
          <InputNumber 
            inputId="quantity" 
            :modelValue="item.quantity"
            @update:modelValue="updateField('quantity', $event)"
            :useGrouping="false"
          />
        </div>
        <div class="field col-6">
          <label for="taxRate">Podatek:</label>
          <InputNumber 
            inputId="taxRate" 
            :modelValue="item.taxRate"
            @update:modelValue="updateField('taxRate', $event)"
            suffix="%"
            :useGrouping="false"
          />
        </div>
        <div class="field col-6">
          <label for="price">Cena:</label>
          <InputNumber
            inputId="price" 
            :modelValue="item.price"
            @update:modelValue="updateField('price', $event)"
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
        @click="$emit('save')" 
      />
    </template>
  </Dialog>
</template>

<script setup>
const props = defineProps({
  visible: Boolean,
  item: Object
})

const emit = defineEmits(['update:visible', 'update:item', 'save'])

const updateField = (field, value) => {
  emit('update:item', { ...props.item, [field]: value })
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
</style>