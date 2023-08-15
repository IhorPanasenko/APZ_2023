import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import RegisterPage from "./pages/reister/RegisterPage";
import "./App.css";
import HomePage from "./pages/home/HomePage";
import CompaniesPage from "./pages/companies/CompaniesPage";
import LoginPage from "./pages/login/LoginPage";
import DetailCompanyPage from "./pages/detailCompany/DetailCompanyPage";
import AgentsPage from "./pages/agentsPage/AgentsPage";
import DetailAgentPage from "./pages/detailAgentPage/DetailAgentPage";
import CategoriesPage from "./pages/categoriesPage/CategoriesPage";
import AboutPage from "./pages/aboutPage/AboutPage";
import PersonalUserPage from "./pages/personalUserPage/PersonalUserPage";
import ForgotPassword from "./pages/forgotPassword/ForgotPassword";
import ResetPassword from "./pages/resetPassword/ResetPassword";
import ManagerMainPage from "./pages/managerMainPage/ManagerMainPage";
import ManagerPersonalPage from "./pages/managerPersonalPage/ManagerPersonalPage";
import ManagerCategoriesPage from "./pages/managerCategoriesPage/ManagerCategoriesPage";
import ManagerPoliciesPage from "./pages/managerPoliciesPage/ManagerPoliciesPage";
import ManagerCompanyUsersPage from "./pages/managerCompanyUsersPage/ManagerCompanyUsersPage";
import ManagerAllUserPolicies from "./pages/managerAllUserPolicies/ManagerAllUserPolicies";
import AdminMainPage from "./pages/adminMainPage/adminMainPage";
import AdminAgentsPage from "./pages/adminAgentsPage/AdminAgentsPage";
import AdminRolePage from "./pages/adminRolePage/AdminRolePage";
import AdminBadHabitPage from "./pages/adminBadHabitPage/AdminBadHabitPage";
import AdminCategoryPage from "./pages/adminCategoriesPage/AdminCategoriesPage";
import AdminCompanyPage from "./pages/adminCompanyPage/AdminCompanyPage";
import AdminPolicyPage from "./pages/adminPolicyPage/AdminPolicyPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/Home" Component={HomePage } />
        <Route path="/Register" Component={RegisterPage} />
        <Route path="/Companies" Component={CompaniesPage} />
        <Route path="/Companies/:companyId" Component={DetailCompanyPage} />
        <Route path="/Agents" Component={AgentsPage} />
        <Route path="/Agents/:agentId" Component={DetailAgentPage} />
        <Route path="Categories" Component={CategoriesPage} />
        <Route path="/About" Component={AboutPage} />
        <Route path="/PersonalUserPage" Component={PersonalUserPage}/>
        <Route path="/ForgotPassword" Component={ForgotPassword}/>
        <Route path="/ResetPassword" Component={ResetPassword}/>
        
        {/* Manager routes */}
        <Route path="/ManagerPersonalPage" Component={ManagerPersonalPage}/>
        <Route path="/ManagerMainPage" Component={ManagerMainPage}/>
        <Route path="/ManageraCategoriesPage" Component={ManagerCategoriesPage} />
        <Route path="/ManagerPoliciesPage" Component={ManagerPoliciesPage} />
        <Route path="/ManagerCompanyUsers" Component={ManagerCompanyUsersPage}/>
        <Route path="/ManagerAllUserPolicies" Component={ManagerAllUserPolicies}/>

        {/*Admin routes*/}
        <Route path="/AdminMainPage" Component={AdminMainPage}/>
        <Route path="/AdminAgentsPage" Component={AdminAgentsPage}/>
        <Route path="/AdminRolePage" Component={AdminRolePage}/>
        <Route path="/AdminBadHabitPage" Component={AdminBadHabitPage}/>
        <Route path="/AdminCategoryPage" Component={AdminCategoryPage}/>
        <Route path="/AdminCompanyPage" Component={AdminCompanyPage}/>
        <Route path="/AdminPolicyPage" Component={AdminPolicyPage}/>

        {/* Default Route */}
        <Route path="/" Component={LoginPage} />
      </Routes>
    </Router>
  );
}

export default App;
