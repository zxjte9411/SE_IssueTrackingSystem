<template>
  <b-table head-variant="light" hover outlined="true" :items="items" :fields="fields"></b-table>
</template>

<script>
export default {
  name: "",
  data() {
    return {
      // Note 'isActive' is left out and will not appear in the rendered table
      fields: [
        {
          key: "id",
          sortable: true
        },
        {
          key: "name",
          sortable: true
        },
        {
          key: "state",
          label: "State",
          sortable: true
          // Variant applies to the whole column, including the header and footer
          // variant: "danger"
        }
      ],
      items: [
        {
          isActive: true,
          state: 40,
          name: "Dickerson",
          id: "0"
        },
        { isActive: false, state: 21, name: "Larsen", id: "1" },
        { isActive: false, state: 89, name: "Geneva", id: "2" },
        { isActive: true, state: 38, name: "Jami", id: "3" }
      ]
    };
  }
};
</script>