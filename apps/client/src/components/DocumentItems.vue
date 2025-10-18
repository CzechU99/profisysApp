<template>
  <table>
    <thead>
      <tr>
        <th>Produkt</th>
        <th>Ilość</th>
        <th>Cena</th>
        <th>Podatek</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="item in items" :key="item.id">
        <td>{{ item.product }}</td>
        <td>{{ item.quantity }}</td>
        <td>{{ item.price }}</td>
        <td>{{ item.taxRate }}</td>
      </tr>
    </tbody>
  </table>
</template>

<script>
export default {
  props: {
    items: {
      type: Array,
      required: true
    }
  }
};
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
  margin-top: 8px;
}
th, td {
  border: 1px solid #ccc;
  padding: 4px;
}
th {
  background-color: #eee;
}
</style>
