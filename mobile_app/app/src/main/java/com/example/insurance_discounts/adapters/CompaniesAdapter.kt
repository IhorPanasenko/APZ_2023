package com.example.insurance_discounts.adapters

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.example.insurance_discounts.R
import com.example.insurance_discounts.data.Company

class CompaniesAdapter() :
    RecyclerView.Adapter<CompaniesAdapter.CompanyViewHolder>() {
    private val companies: MutableList<Company> = mutableListOf()

    fun setCompanies(companies: List<Company>){
        this.companies.clear()
        this.companies.addAll(companies)
        notifyDataSetChanged()
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): CompanyViewHolder {
        val itemView = LayoutInflater.from(parent.context)
            .inflate(R.layout.item_company, parent, false)
        return CompanyViewHolder(itemView)
    }

    override fun onBindViewHolder(holder: CompanyViewHolder, position: Int) {
        val company = companies[position]
        holder.bind(company)
    }

    override fun getItemCount(): Int {
        return companies.size
    }

    inner class CompanyViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        private val companyNameTextView: TextView = itemView.findViewById(R.id.companyNameTextView)
        private val companyAddressTextView: TextView = itemView.findViewById(R.id.companyAddressTextView)
        private val companyPhoneTextView: TextView = itemView.findViewById(R.id.companyPhoneTextView)
        private val companyWebsiteTextView: TextView = itemView.findViewById(R.id.companyWebsiteTextView)
        private val companyMaxDiscountTextView: TextView = itemView.findViewById(R.id.companyMaxDiscountTextView)

        fun bind(company: Company) {
            companyNameTextView.text = company.companyName
            companyAddressTextView.text = company.address
            companyPhoneTextView.text = company.phoneNumber
            companyWebsiteTextView.text = company.websiteAddress
            companyMaxDiscountTextView.text = company.maxDiscountPercentage.toString()

            // Set an OnClickListener or any other interactions as needed
        }
    }
}