<template>
  <ContextMenu ref="cm" :model="menuModel" />
</template>

<script setup>
import { ref, computed } from 'vue'

const emit = defineEmits(['edit', 'delete', 'delete-item'])
const cm = ref()
const selectedData = ref(null)
const selectedType = ref('document') 

const menuModel = computed(() => {
  if (selectedType.value === 'document') {
    return [
      {
        label: 'Usuń',
        icon: 'pi pi-trash',
        command: () => emit('delete', selectedData.value.id)
      },
      {
        label: 'Edytuj',
        icon: 'pi pi-pencil',
        command: () => emit('edit', selectedData.value)
      }
    ]
  } else {
    return [
      {
        label: 'Usuń',
        icon: 'pi pi-trash',
        command: () => emit('delete-item', selectedData.value.id)
      }
    ]
  }
})

const show = (event, data, type = 'document') => {
  selectedData.value = data
  selectedType.value = type
  cm.value.show(event)
}

defineExpose({ show })
</script>
