//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MLearningDB
{
    using System;
    using System.Collections.Generic;
    
    public partial class post_with_username : Post
    {
        public post_with_username()
        {

        }
        public post_with_username(Post other)
        {
            id = other.id;
            text = other.text;
            user_id = other.user_id;
            circle_id = other.circle_id;
            created_at = other.created_at;
            updated_at = other.updated_at;
        }
        public int id { get; set; }
        public string text { get; set; }
        public int user_id { get; set; }
        public int circle_id { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string image_url { get; set; }
    }
}
