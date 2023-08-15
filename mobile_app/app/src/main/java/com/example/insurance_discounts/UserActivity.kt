package com.example.insurance_discounts

import MySharedPreferences
import android.content.Intent
import android.os.Bundle
import android.util.Log
import android.view.MenuItem
import android.widget.Button
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.Toolbar
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView
import com.auth0.android.jwt.JWT
import com.example.insurance_discounts.adapters.UserAdapter
import com.example.insurance_discounts.clients.UserClient
import com.example.insurance_discounts.data.UserInfo
import com.example.insurance_discounts.data.UserInsurance
import com.google.android.material.navigation.NavigationView
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response

class UserActivity : AppCompatActivity(),
    NavigationView.OnNavigationItemSelectedListener{
    private lateinit var userId: String
    private lateinit var firstNameTextView: TextView
    private lateinit var lastNameTextView: TextView
    private lateinit var addressTextView: TextView
    private lateinit var birthdayTextView: TextView
    private lateinit var discountTextView: TextView
    private lateinit var usernameTextView: TextView
    private lateinit var emailTextView: TextView

    private lateinit var updateButton: Button
    private lateinit var userInsurancesRecyclerView: RecyclerView
    private lateinit var userAdapter: UserAdapter

    private lateinit var drawerLayout: DrawerLayout
    private lateinit var sharedPreferences: MySharedPreferences

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_user_info)

        sharedPreferences = MySharedPreferences(this)

        drawerLayout = findViewById<DrawerLayout>(R.id.drawer_layout)
        val toolbar = findViewById<Toolbar>(R.id.toolbar)
        setSupportActionBar(toolbar)
        val navigationView = findViewById<NavigationView>(R.id.nav_view)

        navigationView.setNavigationItemSelectedListener(this)
        val toggle = ActionBarDrawerToggle(
            this,
            drawerLayout,
            toolbar,
            R.string.open_nav,
            R.string.close_nav
        )

        drawerLayout.addDrawerListener(toggle)
        toggle.syncState()

        if(sharedPreferences.getPageId() == null || sharedPreferences.getPageId() == 0){
            navigationView.setCheckedItem(R.id.nav_mainPage)
        }
        else{
            val id = sharedPreferences.getPageId()
            navigationView.setCheckedItem(id!!);
        }

        val token = sharedPreferences.getJwt()
        println(token)
        userId = extractIdFromToken(token!!)!! // Replace with the real userId

        firstNameTextView = findViewById(R.id.firstNameTextView)
        lastNameTextView = findViewById(R.id.lastNameTextView)
        addressTextView = findViewById(R.id.addressTextView)
        birthdayTextView = findViewById(R.id.birthdayTextView)
        discountTextView = findViewById(R.id.discountTextView)
        usernameTextView = findViewById(R.id.usernameTextView)
        emailTextView = findViewById(R.id.emailTextView)

        updateButton = findViewById(R.id.updateButton)
        userInsurancesRecyclerView = findViewById(R.id.userInsurancesRecyclerView)

        userAdapter = UserAdapter()
        userInsurancesRecyclerView.adapter = userAdapter

        getUserInfo(userId)
        getUserInsurances(userId)

        updateButton.setOnClickListener {
            val intent = Intent(this, UpdateUserActivity::class.java)
            startActivity(intent)
        }
    }

    private fun extractIdFromToken(token: String): String? {
        try {
            // Decode the JWT token
            val jwt = JWT(token)
            return jwt.getClaim("UserId").asString()
        } catch (e: Exception) {
            e.printStackTrace()
        }
        return null
    }

    private fun getUserInfo(userId: String) {
        val userService = UserClient.create()
        val call = userService.getUserInfo(userId)

        call.enqueue(object : Callback<UserInfo> {
            override fun onResponse(call: Call<UserInfo>, response: Response<UserInfo>) {
                if (response.isSuccessful) {
                    val userInfo = response.body()
                    userInfo?.let {
                        // Display user info
                        firstNameTextView.text = "Name: ${userInfo.firstName}"
                        lastNameTextView.text = "Last name: ${userInfo.lastName}"
                        addressTextView.text =  "Address: ${userInfo.address}"
                        birthdayTextView.text = "Birthday: ${userInfo.birthdayDate}"
                        discountTextView.text = "Discount: ${userInfo.discount}"
                        usernameTextView.text = "Username: ${userInfo.userName}"
                        emailTextView.text = "Email: ${userInfo.email}"
                    }
                }
            }

            override fun onFailure(call: Call<UserInfo>, t: Throwable) {
                Toast.makeText(
                    this@UserActivity,
                    "Error fetching user info",
                    Toast.LENGTH_SHORT
                ).show()
            }
        })
    }

    private fun getUserInsurances(userId: String) {
        val userService = UserClient.create()
        val call = userService.getUserInsurances(userId)

        call.enqueue(object : Callback<List<UserInsurance>> {
            override fun onResponse(
                call: Call<List<UserInsurance>>,
                response: Response<List<UserInsurance>>
            ) {
                if (response.isSuccessful) {
                    val userInsurances = response.body()
                    userInsurances?.let {
                        // Display user insurances
                        userAdapter.setUserInsurances(userInsurances)
                        //userAdapter = UserAdapter(userInsurances)
                        userInsurancesRecyclerView.layoutManager = LinearLayoutManager(this@UserActivity)
                        userInsurancesRecyclerView.adapter = userAdapter
                    }
                }
            }

            override fun onFailure(call: Call<List<UserInsurance>>, t: Throwable) {
                Toast.makeText(
                    this@UserActivity,
                    "Error fetching user insurances",
                    Toast.LENGTH_SHORT
                ).show()
                Log.e("CategoriesActivity", "Error: ${t.message}", t)
            }
        })
    }

    override fun onNavigationItemSelected(item: MenuItem): Boolean {
        if(item.itemId == R.id.nav_personalPage){
            val intent = Intent(this, UserActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_mainPage){
            val intent = Intent(this, MainPageActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_companies){
            val intent = Intent(this, CompaniesActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_agents){
            val intent = Intent(this, AgentsActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_categories){
            val intent = Intent(this, CategoriesActivity::class.java)
            startActivity(intent)
        }

        if(item.itemId == R.id.nav_about){
            val intent = Intent(this, AboutUsActivity::class.java)
            startActivity(intent);
        }

        if(item.itemId == R.id.nav_logout){
            sharedPreferences.clearJwt()
            sharedPreferences.clearPageId()
            val intent = Intent(this, MainActivity::class.java)
            startActivity(intent)
        }

        sharedPreferences.savePageId(item.itemId)
        drawerLayout.closeDrawer(GravityCompat.START)
        return true
    }

    override fun onBackPressed() {
        if (drawerLayout.isDrawerOpen(GravityCompat.START)) {
            drawerLayout.closeDrawer(GravityCompat.START)
        } else {
            onBackPressedDispatcher.onBackPressed()
        }
    }
}