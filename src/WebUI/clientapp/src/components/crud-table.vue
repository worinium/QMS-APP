<template>
  <div>
    <b-row>
      <b-col lg="3" class="my-2">
        <b-form-group label="Search" label-cols-sm="2" label-align-sm="right"
          label-size="sm"
          label-for="filterInput"
          class="mb-0"
        >
          <b-input-group size="sm">
            <b-form-input
              v-model="filter"
              type="search"
              id="filterInput"
              placeholder="Type to Search"
            ></b-form-input>
          </b-input-group>
        </b-form-group>
      </b-col>

      <b-col lg="2" class="my-2">
        <b-form-group
          label="Filter"
          label-cols-sm="2"
          label-align-sm="right"
          label-size="sm"
          label-for="initialSortSelect"
          class="mb-0"
        >
          <b-form-select
            v-model="activeFilterStatus"
            id="initialSortSelect"
            size="sm"
            :options="statusFilterOptions"
          ></b-form-select>
        </b-form-group>
      </b-col>

      <!--Start of Selection Mode -->
      <b-col lg="4" class="my-2">
        <b-form-group label="Selection Mode:" label-size="sm" label-cols-sm="3">
          <b-form-select v-model="selectMode" size="sm" :options="modes"></b-form-select>
        </b-form-group>
      </b-col>
      <!--End Selection Mode -->
      <b-col lg="3" class="my-1">
        <b-button size="sm" @click="selectAllRows" variant="outline-primary">Select all</b-button>&nbsp;
        <b-button size="sm" @click="clearSelected" variant="outline-primary">Clear selected</b-button>&nbsp;
        <b-button @click="createItem" variant="primary" size="sm">
          <vue-fontawesome icon="plus" size="1" color="lightgray"></vue-fontawesome>&nbsp;&nbsp;Add Message
        </b-button>
      </b-col>
    </b-row>
    <!-- Table Region -->
    <b-table
      striped
      table-variant="info"
      hover
      :items="tableData"
      :fields="columns"
      :current-page="currentPage"
      :per-page="perPage"
      :filter="filter"
      :filterIncludedFields="filterOn"
      :sort-by.sync="sortBy"
      :sort-desc.sync="sortDesc"
      :sort-direction="sortDirection"
      @filtered="onFiltered"
      :busy.sync="isBusy"
      ref="selectableTable"
      selectable
      :select-mode="selectMode"
      @row-selected="onRowSelected"
      responsive="sm"
      small
      primary-key="subject"
      :tbody-transition-props="transProps"
    >
      <template v-slot:table-busy>
        <div class="text-center text-primary my-2">
          <b-spinner class="align-middle"></b-spinner>
          <strong>Loading...</strong>
        </div>
      </template>

      <template v-slot:cell(name)="row">{{ row.value.first }} {{ row.value.last }}</template>

      <template v-slot:cell(selected)="{ rowSelected }">
        <template v-if="rowSelected">
          <span aria-hidden="true">&check;</span>
          <span class="sr-only">Selected</span>
        </template>
        <template v-else>
          <span aria-hidden="true">&nbsp;</span>
          <span class="sr-only">Not selected</span>
        </template>
      </template>

      <template #cell(action)="data">
        <b-button @click="markasRead(data.item)" variant="success" size="sm">
          <span v-if="data.item.status === 'read'">Mark As UnRead</span>
          <span v-else>Mark As Read</span>
        </b-button>&nbsp;
        <b-button @click="markAsImportant(data.item)" variant="primary" size="sm">
          <span v-if="!data.item.important">Mark As Important</span>
          <span v-else>Mark As Not Important</span>
        </b-button>&nbsp;
        <b-button
          @click="deleteItem(data.item)"
          v-b-modal="'edit-modal'"
          variant="danger"
          size="sm"
        >Delete</b-button>&nbsp;
        <b-button
          size="sm"
          @click="data.toggleDetails"
          variant="info"
        >{{ data.detailsShowing ? 'Hide' : 'Show' }} Details</b-button>
      </template>

      <template v-slot:row-details="row">
        <b-card>
          <ul>
            <li v-for="(value, key) in row.item" :key="key">{{ key }}: {{ value }}</li>
          </ul>
        </b-card>
      </template>
    </b-table>
    <!--Pagination-->
    <b-row>
      <b-col sm="2" md="4" class="my-1">
        <b-form-group
          label="Per page"
          label-cols-sm="6"
          label-cols-md="4"
          label-cols-lg="2"
          label-align-sm="right"
          label-size="sm"
          label-for="perPageSelect"
          class="mb-0"
        >
          <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
        </b-form-group>
      </b-col>

      <b-col sm="10" md="8" class="my-1">
        <b-pagination
          pills
          v-model="currentPage"
          :total-rows="totalRows"
          :per-page="perPage"
          align="fill"
          size="sm"
          class="my-0"
        ></b-pagination>
      </b-col>
    </b-row>
    <!-- Modal Section for Sending notification -->
    <b-modal v-model="modalShow" :title="formTitle" hide-footer>
      <b-form @submit.prevent="save">
        <slot :formdata="editedItem" name="input-fields"></slot>
        <b-button size="sm" variant="danger" @click="close">Cancel</b-button>&nbsp;
        <b-button type="submit" size="sm" variant="success">Send</b-button>
      </b-form>
    </b-modal>
  </div>
</template>

<script>
export default {
  props: ["endpoint", "columns", "formFields"],
  data() {
    return {
      activeFilterStatus: "",
      statusFilterOptions: [
        { value: "", text: "All" },
        { value: "read", text: "Read" },
        { value: "unread", text: "UnRead" },
        { value: true, text: "Important" }
      ],
      transProps: {
        // Transition name
        name: "flip-list"
      },
      // Multiple Selection
      modes: ["multi", "single", "range"],
      selectMode: "multi",
      selected: [],
      editedItem: this.formFields,
      modalShow: false,
      editedIndex: -1,
      tableData: [],
      currentPage: 1,
      perPage: 30,
      pageOptions: [30, 60, 90, 120, 150, 180, 210, 240, 270, 300],
      sortBy: "",
      sortDesc: false,
      sortDirection: "asc",
      filter: null,
      filterOn: [],
      isBusy: false
    };
  },
  computed: {
    totalRows() {
      return this.tableData.length > 0 ? this.tableData.length : 1;
    },
    formTitle() {
      // return this.editedIndex === -1 ? "Send Notification" : "Edit Item";
      return "Send Notification";
    },
    sortOptions() {
      // Create an options list from our fields
      return this.fields
        .filter(f => f.sortable)
        .map(f => {
          return { text: f.label, value: f.key };
        });
    }
  },
  mounted() {
    // Set the initial number of items
    this.totalRows = this.tableData.length;
    //  Console.log("MountedMethod", this.totalRows)
  },
  watch: {
    activeFilterStatus(newVal) {
      if (typeof newVal === "string" && newVal !== "") {
        this.tableData = this.$store.state.messages.filter(
          x => x.status === newVal
        );
      } else if (typeof newVal === "boolean") {
        this.tableData = this.$store.state.messages.filter(
          x => x.important === newVal
        );
      } else {
        this.tableData = this.$store.state.messages;
      }
    }
  },

  methods: {
    toggleBusy() {
      setTimeout(() => {
        this.isBusy = !this.isBusy;
      }, 200);
    },

    markasRead(item) {
      let sample = item.status === "read" ? "unread" : "read";
      const data = { ...item, status: sample };
      this.$store.dispatch("updateMessage", data);
    },
    markAsImportant(item) {
      const data = { ...item, important: !item.important };
      this.$store.dispatch("updateMessage", data);
    },
    onRowSelected(items) {
      this.selected = items;
    },
    selectAllRows() {
      this.$refs.selectableTable.selectAllRows();
    },
    clearSelected() {
      this.$refs.selectableTable.clearSelected();
    },

    createItem() {
      this.modalShow = true;
      this.editedItem = Object.assign({}, this.formFields);
      this.editedIndex = -1;
    },
    
    onFiltered(filteredItems) {
      console.log("onFiltered", filteredItems);
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },

    deleteItem(item) {
      //   const index = this.tableData.indexOf(item);
      if (confirm("Are you sure you want to delete this item?")) {
        this.$store
          .dispatch("removeMessage", item)
          .then(() => this.fetchNotification());
      }
    },
    close() {
      this.modalShow = false;
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.formFields);
        this.editedIndex = -1;
      }, 300);
    },
    save() {
      if (this.editedIndex > -1) {
        Object.assign(this.tableData[this.editedIndex], this.editedItem);
        this.$http.put(
          this.endpoint + "/" + this.editedItem.id,
          this.editedItem
        );
      } else {
        const data = { ...this.editedItem };
        this.$store
          .dispatch("addMessage", data)
          .then(() => this.fetchNotification());
      }
      this.close();
    },
    fetchNotification() {
      this.$store.dispatch("fetchAllNotifications").then(() => {
        this.toggleBusy();
        this.tableData = this.$store.state.messages.reverse();
        this.isBusy = !this.isBusy;
        console.log("tableData", this.tableData);
      });
    }
  },
  created() {
    this.fetchNotification();
  }
};
</script>
<style scoped>
.table-container {
  position: relative;
  width: 100%;
}
.b-table[aria-busy="true"] + .table-spinner {
  /* this assumes that the spinner component has a width and height */
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate(-50%, -50%);
  z-index: 10; /* make sure spinner is over table */
  opacity: 0.6;
}
</style>
