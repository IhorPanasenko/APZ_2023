package com.example.insurance_discounts

import MySharedPreferences
import android.content.Intent
import android.os.Bundle
import android.view.MenuItem
import android.widget.ImageView
import android.widget.LinearLayout
import android.widget.TextView
import androidx.appcompat.app.ActionBarDrawerToggle
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.Toolbar
import androidx.core.view.GravityCompat
import androidx.drawerlayout.widget.DrawerLayout
import com.google.android.material.navigation.NavigationView

class AboutUsActivity : AppCompatActivity(),
    NavigationView.OnNavigationItemSelectedListener{
    private lateinit var drawerLayout: DrawerLayout
    private lateinit var sharedPreferences: MySharedPreferences


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_about_us)

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

        // Set the title of the activity
        supportActionBar?.title = getString(R.string.about_us_title)

        // Set up the views and populate the data
        setUpIntroduction()
        setUpBenefits()
        setUpSatisfiedCustomers()
    }

    private fun setUpIntroduction() {
        val introductionTitle = findViewById<TextView>(R.id.introductionTitle)
        val introductionImage = findViewById<ImageView>(R.id.introductionImage)
        val introductionDescription = findViewById<TextView>(R.id.introductionDescription)

        introductionTitle.text = getString(R.string.introduction_title)
        introductionImage.setImageResource(R.drawable.baseline_accessible_forward_24)
        introductionDescription.text = getString(R.string.introduction_description)
    }

    private fun setUpBenefits() {
        val benefitsTitle = findViewById<TextView>(R.id.benefitsTitle)
       // val benefitsList = findViewById<LinearLayout>(R.)

//        benefitsTitle.text = getString(R.string.benefits_title)
//
//        val benefitsItems = listOf(
//            getString(R.string.benefits_item1),
//            getString(R.string.benefits_item2),
//            getString(R.string.benefits_item3)
//        )
//
//        for (item in benefitsItems) {
//            val textView = TextView(this)
//            textView.text = item
//            benefitsList.addView(textView)
//        }
    }

    private fun setUpSatisfiedCustomers() {
        val satisfiedCustomersTitle = findViewById<TextView>(R.id.satisfiedCustomersTitle)
        val satisfiedCustomersImage = findViewById<ImageView>(R.id.satisfiedCustomersImage)
        val customer1Name = findViewById<TextView>(R.id.customer1Name)
        val customer1Feedback = findViewById<TextView>(R.id.customer1Feedback)
        val customer2Name = findViewById<TextView>(R.id.customer2Name)
        val customer2Feedback = findViewById<TextView>(R.id.customer2Feedback)

        satisfiedCustomersTitle.text = getString(R.string.satisfied_customers_title)
        satisfiedCustomersImage.setImageResource(R.drawable.baseline_directions_walk_24)
        customer1Name.text = getString(R.string.customer1_name)
        customer1Feedback.text = getString(R.string.customer1_feedback)
        customer2Name.text = getString(R.string.customer2_name)
        customer2Feedback.text = getString(R.string.customer2_feedback)
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
