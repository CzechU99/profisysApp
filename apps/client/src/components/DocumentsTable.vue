<template>
  <div>
    <h1>Lista dokumentów</h1>
    <table>
      <thead>
        <tr>
          <th>Id</th>
          <th>Typ</th>
          <th>Data</th>
          <th>Imię</th>
          <th>Nazwisko</th>
          <th>Miasto</th>
          <th>Pozycje</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="doc in documents" :key="doc.id">
          <td>{{ doc.id }}</td>
          <td>{{ doc.type || '-' }}</td>
          <td>{{ doc.date }}</td>
          <td>{{ doc.firstName }}</td>
          <td>{{ doc.lastName }}</td>
          <td>{{ doc.city }}</td>
          <td>
            <button @click="toggle(doc.id)">
              {{ expanded[doc.id] ? 'Ukryj' : 'Pokaż' }}
            </button>
            <DocumentItems v-if="expanded[doc.id]" :items="doc.documentItem" />
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue';
import { getAllDocuments } from '../api/documentsService';
import DocumentItems from './DocumentItems.vue';

export default {
  components: { DocumentItems },
  setup() {
    const documents = ref([]);
    const expanded = ref({});

    const fetchDocuments = async () => {
      try {
        documents.value = await getAllDocuments();
      } catch (error) {
        alert('Nie udało się pobrać dokumentów');
      }
    };

    const toggle = (id) => {
      expanded.value[id] = !expanded.value[id];
    };

    onMounted(fetchDocuments);

    return { documents, expanded, toggle };
  },
};
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
}
th, td {
  border: 1px solid #ddd;
  padding: 8px;
}
th {
  background-color: #f4f4f4;
}
button {
  padding: 4px 8px;
  cursor: pointer;
}
</style>
