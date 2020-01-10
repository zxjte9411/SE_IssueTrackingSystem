<template>
  <div>
    <NavBar />
    <b-container fluid class="mt-3">
      <b-row>
        <b-col lg="4" class="my-1">
          <h1>專案管理</h1>
        </b-col>
        <b-col lg="8" class="my-1"></b-col>
      </b-row>
      <b-card>
        <!-- User Interface controls -->
        <b-row class="mb-4">
          <b-col lg="6" class="my-1">
            <b-form-group
              label="Sort"
              label-cols-sm="3"
              label-align-sm="right"
              label-size="sm"
              label-for="sortBySelect"
              class="mb-0"
            >
              <b-input-group size="sm">
                <b-form-select
                  v-model="sortBy"
                  id="sortBySelect"
                  :options="sortOptions"
                  class="w-75"
                >
                  <template v-slot:first>
                    <option value>-- none --</option>
                  </template>
                </b-form-select>
                <b-form-select v-model="sortDesc" size="sm" :disabled="!sortBy" class="w-25">
                  <option :value="false">Asc</option>
                  <option :value="true">Desc</option>
                </b-form-select>
              </b-input-group>
            </b-form-group>
          </b-col>
          <!-- User Interface controls END -->
          <!-- 初始排序方式 -->
          <b-col lg="6" class="my-1">
            <b-form-group
              label="Initial sort"
              label-cols-sm="3"
              label-align-sm="right"
              label-size="sm"
              label-for="initialSortSelect"
              class="mb-0"
            >
              <b-form-select
                v-model="sortDirection"
                id="initialSortSelect"
                size="sm"
                :options="['asc', 'desc', 'last']"
              ></b-form-select>
            </b-form-group>
          </b-col>
          <!-- 初始排序方式 END-->
          <!-- 輸入搜尋 -->
          <b-col lg="6" class="my-1">
            <b-form-group
              label="Filter"
              label-cols-sm="3"
              label-align-sm="right"
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
                <b-input-group-append>
                  <b-button :disabled="!filter" @click="filter = ''">Clear</b-button>
                </b-input-group-append>
              </b-input-group>
            </b-form-group>
          </b-col>
          <!-- 輸入搜尋 END -->
          <!-- 勾選要過濾的欄位 -->
          <b-col lg="6" class="my-1">
            <b-form-group
              label="Filter On"
              label-cols-sm="3"
              label-align-sm="right"
              label-size="sm"
              description="Leave all unchecked to filter on all data"
              class="mb-0"
            >
              <b-form-checkbox-group v-model="filterOn" class="mt-1">
                <b-form-checkbox value="name">專案名稱</b-form-checkbox>
                <b-form-checkbox value="id">專案序號</b-form-checkbox>
                <b-form-checkbox value="manager">管理者</b-form-checkbox>
                <b-form-checkbox value="developers">開發成員</b-form-checkbox>
              </b-form-checkbox-group>
            </b-form-group>
          </b-col>
          <!-- 勾選要過濾的欄位 END -->

          <!-- 設定每頁顯示數量 下拉選單-->
          <b-col sm="5" md="6" class="my-1">
            <b-form-group
              label="Per page"
              label-cols-sm="6"
              label-cols-md="4"
              label-cols-lg="3"
              label-align-sm="right"
              label-size="sm"
              label-for="perPageSelect"
              class="mb-0"
            >
              <b-form-select v-model="perPage" id="perPageSelect" size="sm" :options="pageOptions"></b-form-select>
            </b-form-group>
          </b-col>
          <!-- 設定每頁顯示數量 END-->

          <!-- 選擇頁數按鈕 -->
          <b-col sm="7" md="6" class="my-1">
            <b-pagination
              v-model="currentPage"
              :total-rows="totalRows"
              :per-page="perPage"
              align="fill"
              size="sm"
              class="my-0"
            ></b-pagination>
          </b-col>
          <!-- 選擇頁數按鈕 END -->
          <b-col lg="6" class="my-2">
            <b-button
              class="float-left"
              block
              pill
              variant="outline-danger"
              size="lg"
              @click="openAddModal"
            >新增專案</b-button>
          </b-col>
        </b-row>
        <!-- Main table element -->
        <b-table
          show-empty
          striped
          small
          stacked="md"
          :items="projects"
          :fields="fields"
          :current-page="currentPage"
          :per-page="perPage"
          :filter="filter"
          :filterIncludedFields="filterOn"
          :sort-by.sync="sortBy"
          :sort-desc.sync="sortDesc"
          :sort-direction="sortDirection"
          @filtered="onFiltered"
        >
          <template v-slot:cell(developers)="row">
            <b-button size="sm" @click="showDevelopers(row.item)" variant="info" class="mr-1">詳細成員</b-button>
          </template>
          <template v-slot:cell(generals)="row">
            <b-button size="sm" @click="showGenerals(row.item)" variant="info" class="mr-1">詳細成員</b-button>
          </template>
          <template v-slot:cell(issues)="row">
            <b-button size="sm" @click="showIssues(row.item)" variant="info" class="mr-1">Issues</b-button>
          </template>
          <template v-slot:cell(actions)="row">
            <b-button
              size="sm"
              @click="setData(row.item, row.index, $event.target)"
              variant="success"
              class="mr-1"
            >編輯</b-button>
            <b-button
              size="sm"
              @click="row.toggleDetails"
              variant="info"
              class="mr-1"
            >{{ row.detailsShowing ? '隱藏' : '顯示' }}詳細</b-button>
            <b-button
              size="sm"
              @click="deleteItem=row.item"
              v-b-modal.check-delete-modal
              variant="danger"
            >
              <font-awesome-icon icon="trash-alt" />
            </b-button>
          </template>
          <template v-slot:row-details="row">
            <b-card>
              <b-list-group>
                <b-list-group-item
                  class="text-left"
                  button
                  v-for="(value, key) in row.item"
                  :key="key"
                >{{ key }}: {{ value }}</b-list-group-item>
              </b-list-group>
            </b-card>
          </template>
        </b-table>
      </b-card>
    </b-container>
    <!-- Add new Project -->
    <b-modal id="addProjectModal" title="新增專案" @ok="createProject">
      <div class="modal-body">
        <div class="row">
          <div class="col-sm">
            <div class="form-group">
              <label for="title">問題名稱</label>
              <input
                type="text"
                class="form-control"
                id="title"
                placeholder
                required
                v-model="tempProject.name"
              />
            </div>
            <div class="form-group">
              <label for="description">專案描述</label>
              <textarea
                type="text"
                class="form-control"
                id="description"
                placeholder
                v-model="tempProject.description"
              ></textarea>
            </div>
          </div>
        </div>
      </div>
    </b-modal>
    <!-- Info modal -->
    <b-modal :id="infoModal.id" title="編輯" hide-footer @hide="resetInfoModal" centered>
      <b-form ref="form" @submit.stop.prevent="handleSubmit">
        <b-container fluid>
          <b-row>
            <b-form-group
              id="project-name-group"
              label="專案名稱"
              label-for="project"
              description="專案名稱"
            >
              <b-form-input id="project" v-model="newData.name" required />
            </b-form-group>
          </b-row>
          <b-row>
            <b-col class="my-1">
              <b-form-group
                id="developer-group"
                label="開發成員"
                label-for="developer"
                description="編輯開發成員"
              >
                <b-form-checkbox-group
                  v-model="developersSelected"
                  :options="developersOptions"
                  stacked
                ></b-form-checkbox-group>
              </b-form-group>
            </b-col>
            <b-col class="my-1">
              <b-form-group
                id="generals-group"
                label="一般使用者"
                label-for="generals"
                description="編輯一般使用者"
              >
                <b-form-checkbox-group
                  v-model="generalsSelected"
                  :options="generalsOptions"
                  stacked
                ></b-form-checkbox-group>
              </b-form-group>
            </b-col>
          </b-row>
          <b-button type="submit" pill variant="success" class="float-right">送出</b-button>
        </b-container>
      </b-form>
    </b-modal>
    <!-- Info modal END-->
    <!-- check delete modal-->
    <b-modal id="check-delete-modal" hide-footer>
      <b-alert show variant="warning" class="text-center">確定刪除？</b-alert>
      <b-button class="float-right" @click="deleteProject(false)">取消</b-button>
      <b-button variant="danger" class="float-right mr-2" @click="deleteProject(true)">確定</b-button>
    </b-modal>
    <!-- check delete modal END-->
    <b-modal id="show-developers-Modal" title="詳細成員" @hide="resetInfoModal" centered ok-only>
      <b-card>
        <b-list-group>
          <b-list-group-item
            button
            v-for="(developer,　key) in developers"
            :key="key"
          >{{developer.name}}</b-list-group-item>
        </b-list-group>
      </b-card>
    </b-modal>
    <b-modal id="show-generals-Modal" title="詳細成員" @hide="resetInfoModal" centered ok-only>
      <b-card>
        <b-list-group>
          <b-list-group-item button v-for="(general, key) in generals" :key="key">{{general.name}}</b-list-group-item>
        </b-list-group>
      </b-card>
    </b-modal>
    <b-modal id="show-issues-Modal" title="此專案下的 Issue" @hide="resetInfoModal" centered ok-only>
      <b-card>
        <b-list-group>
          <b-list-group-item button v-for="(issue, key) in issuesOfProject" :key="key">{{issue}}</b-list-group-item>
        </b-list-group>
      </b-card>
    </b-modal>
    <!-- <b-button @click="testeverything">testbutton</b-button> -->
  </div>
</template>

<script>
import NavBar from "@/components/NavBar.vue";
import axios from "axios";
import { async } from "q";

export default {
  data() {
    return {
      tempProject: {
        name: "",
        description: "",
        managerId: ""
      },
      newData: {
        name: "",
        id: "",
        userId: "",
        manager: "",
        developers: [],
        generals: []
      },
      developers: [],
      generals: [],
      developersSelected: [],
      developersOptions: [],
      generalsSelected: [],
      generalsOptions: [],
      users: [],
      issues: [],
      issuesOfProject: [],
      projects: [],
      fields: [
        {
          key: "name",
          label: "專案名稱名稱",
          sortable: true,
          sortDirection: "desc"
        },
        {
          key: "id",
          label: "專案序號",
          sortable: true,
          class: "text-center"
        },
        {
          key: "manager",
          label: "管理者",
          sortable: true,
          sortDirection: "desc",
          class: "text-center",
          formatter: (value /*, key, item*/) => {
            return value ? value["name"] : "";
          }
        },
        { key: "developers", label: "開發成員" },
        { key: "generals", label: "一般使用者" },
        { key: "issues", label: "Issues" },
        { key: "actions", label: "操作" }
      ],
      totalRows: 1,
      currentPage: 1,
      perPage: 10,
      pageOptions: [10, 15, 20],
      sortBy: "id",
      sortDesc: false,
      sortDirection: "asc",
      filter: null,
      filterOn: [],
      infoModal: {
        id: "info-modal",
        title: "",
        content: ""
      },
      deleteItem: null
    };
  },
  computed: {
    sortOptions() {
      // Create an options list from our fields
      return this.fields
        .filter(f => f.sortable)
        .map(f => {
          return { text: f.label, value: f.key };
        });
    }
  },
  components: {
    NavBar
  },
  mounted() {
    this.fetchData(); // Set the initial number of projects
    this.getAllUser();
    this.getAllIssue();
  },
  methods: {
    setData(item /*, index, button*/) {
      this.infoModal.title = item.name;
      this.newData.id = item.id;
      this.newData.name = item.name;
      this.newData.manager = item.manager;
      this.newData.developers = item.developers;
      this.newData.generals = item.generals;
      this.infoModal.content = JSON.stringify(item, null, 2);
      // this.$root.$emit("bv::show::modal", this.infoModal.id, button);
      let developers = [];
      let generals = [];
      const vm = this;
      this.developersOptions = [];
      this.developersSelected = [];
      this.generalsOptions = [];
      this.generalsSelected = [];
      this.users.forEach(function(user) {
        // 1 是開發者
        if (user.charactorId == 1) {
          developers.push(user.id);
          vm.developersOptions.push({ text: user.name, value: user.id });
        } else {
          generals.push(user.id);
          vm.generalsOptions.push({ text: user.name, value: user.id });
        }
      });
      this.newData.developers.forEach(function(developer) {
        if (developers.includes(developer.id))
          vm.developersSelected.push(developer.id);
      });
      this.newData.generals.forEach(function(general) {
        if (generals.includes(general.id)) vm.generalsSelected.push(general.id);
      });
      this.$bvModal.show(this.infoModal.id);
    },
    resetInfoModal() {
      this.infoModal.title = "";
      this.infoModal.content = "";
      this.newData = {
        name: "",
        id: "",
        userId: "",
        manager: "",
        developers: [],
        generalss: []
      };
    },
    onFiltered(filteredItems) {
      // Trigger pagination to update the number of buttons/pages due to filtering
      this.totalRows = filteredItems.length;
      this.currentPage = 1;
    },
    async fetchData() {
      const token = localStorage.getItem("token");
      let res = await axios
        .get("http://lspssapple.asuscomm.com:81/api/project", {
          headers: {
            "content-type": "application/json;charset=utf-8",
            Authorization: `Bearer ${token}`
          }
        })
        .then(async res => {
          return await res;
        })
        .catch(async err => {
          return await err.response;
        });
      if (res.status == 401) {
        localStorage.removeItem("token", res.data.token);
        localStorage.removeItem("user_id", res.data.userId);
        this.$router.push("/");
      }
      this.projects = res.data;
      this.totalRows = this.projects.length;
    },
    async deleteProject(isCanDelete) {
      this.$bvModal.hide("check-delete-modal");
      if (isCanDelete) {
        const token = localStorage.getItem("token");
        let res = await axios
          .delete(
            `http://lspssapple.asuscomm.com:81/api/project/${this.deleteItem.id}`,
            {
              headers: {
                "content-type": "application/json;charset=utf-8",
                Authorization: `Bearer ${token}`
              }
            }
          )
          .then(async res => {
            this.fetchData();
            this.deleteItem = null;
            return await res;
          })
          .catch(async err => {
            return await err.response;
          });
      }
    },
    async handleSubmit() {
      const data = {
        name: this.newData.name,
        managerId: this.newData.manager.id,
        developersId: this.developersSelected,
        generalsId: this.generalsSelected
      };
      const token = localStorage.getItem("token");
      let res = await axios
        .post(
          `http://lspssapple.asuscomm.com:81/api/project/${this.newData.id}`,
          data,
          {
            headers: {
              "content-type": "application/json;charset=utf-8",
              Authorization: `Bearer ${token}`
            }
          }
        )
        .then(async res => {
          return await res;
        })
        .catch(async err => {
          console.log(err);
          return await err.response;
        });
      if (res.status == 200) {
        this.fetchData();
        this.$bvModal.hide(this.infoModal.id);
        this.makeToast("success", "編輯成功！", "成功");
      } else {
        localStorage.removeItem("token", res.data.token);
        localStorage.removeItem("user_id", res.data.userId);
        this.$router.push("/");
      }
    },
    createProject() {
      const api = "http://lspssapple.asuscomm.com:81/api/project";
      const vm = this;
      const token = localStorage.getItem("token");
      vm.tempProject.managerId = parseInt(localStorage.getItem("user_id"));
      axios
        .post(api, vm.tempProject, {
          headers: {
            Authorization: `Bearer ${token}`,
            "content-type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          if (response.status == 200) {
            vm.tempProject = { name: "", description: "", managerId: "" };
            this.makeToast("success", "成功", "成功新增專案!");
            this.fetchData();
          }
        })
        .catch(err => {
          this.makeToast("danger", "失敗", "專案新增失敗!");
        });
    },
    openAddModal() {
      this.$bvModal.show("addProjectModal");
    },
    showDevelopers(item) {
      this.infoModal.title = "詳細成員";
      this.developers = item.developers;
      this.$bvModal.show("show-developers-Modal");
    },
    showGenerals(item) {
      this.infoModal.title = "詳細成員";
      this.generals = item.generals;
      this.$bvModal.show("show-generals-Modal");
    },
    showIssues(item) {
      this.infoModal.title = "此專案下的 Issue";
      this.issuesOfProject = [];
      const vm = this;
      this.issues.forEach(function(issue) {
        if (issue.id == item.id)
          vm.issuesOfProject.push(`Numbe${issue.number}-${issue.summary}`);
      });
      this.$bvModal.show("show-issues-Modal");
    },
    getAllUser() {
      const api = "http://lspssapple.asuscomm.com:81/api/user";
      const vm = this;
      const token = localStorage.getItem("token");
      axios
        .get(api, {
          headers: {
            Authorization: "Bearer " + token,
            "content-type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          if (response.status == 200) {
            response.data.forEach(user =>
              vm.users.push({
                id: user.id,
                name: user.name,
                charactorId: user.charactorId
              })
            );
          }
        });
    },
    getAllIssue() {
      const api = "http://lspssapple.asuscomm.com:81/api/issue";
      const vm = this;
      const token = localStorage.getItem("token");
      axios
        .get(api, {
          headers: {
            Authorization: "Bearer " + token,
            "content-type": "application/json;charset=utf-8"
          }
        })
        .then(response => {
          if (response.status == 200) {
            response.data.forEach(issue => {
              vm.issues.push({
                id: issue.projectId,
                number: issue.number,
                summary: issue.summary,
                createUserId: issue.createUser
              });
            });
          }
        });
    },
    makeToast(
      variant = null,
      message = "",
      title = "",
      toaster = "b-toaster-top-right"
    ) {
      this.$bvToast.toast(message, {
        title: title,
        toaster: toaster,
        variant: variant,
        solid: true
      });
    },
    testeverything() {
      console.log(this.issues);
    }
  }
};
</script>