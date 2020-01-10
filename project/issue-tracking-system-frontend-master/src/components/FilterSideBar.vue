<template>
  <div>
    <nav class="col-md-2 d-none d-md-block bg-light sidebar">
      <div class="sidebar-sticky">
        <ul class="nav flex-column">
          <li class="nav-item">
            <a class="nav-link linkcursor" :class="{'active': selected == '首頁'}" @click.prevent="$router.push('/cobar/home'); selected = '首頁'">
              首頁
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link linkcursor" :class="{'active': selected == '找飲料'}" @click.prevent="$router.push('/cobar/product'); selected = '找飲料'">
              找飲料
            </a>
          <li class="nav-item">
            <a class="nav-link linkcursor" :class="{'active': selected == '會員資料', 'hidden': !IsLogin}" @click.prevent="$router.push('/member/account'); selected = '會員資料'">
              會員資料
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link linkcursor" :class="{'active': selected == '產品管理', 'hidden': memberType != 2}" @click.prevent="$router.push('/member/productManagement'); selected = '產品管理'">
              產品管理
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link linkcursor" :class="{'active': selected == '購買紀錄', 'hidden': memberType != 1}" @click.prevent="$router.push('/member/customerOrder'); selected = '購買紀錄'">
              購買紀錄
            </a>
          </li>
        </ul>

        <!-- <h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
          <span>Saved reports</span>
          <a class="d-flex align-items-center text-muted" href="#">
            <span data-feather="plus-circle"></span>
          </a>
        </h6> -->
        <ul class="nav flex-column mb-2">
          <!-- <li class="nav-item">
            <a class="nav-link linkcursor" @click.prevent="check()">
              檢查
            </a>
          </li> -->
          <li class="nav-item">
            <a class="nav-link linkcursor">
              聯絡我們
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link linkcursor">
              回報問題
            </a>
          </li>
        </ul>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: 'FilterSideBar',
  data (){
      return{
        selected: '首頁'
      };
    },
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>

</style>
