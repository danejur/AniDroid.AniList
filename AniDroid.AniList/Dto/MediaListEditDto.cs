﻿using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.Models;

namespace AniDroid.AniList.Dto
{
    public class MediaListEditDto
    {
        public int MediaId { get; set; }
        public Media.MediaListStatus Status { get; set; }
        public float? Score { get; set; }
        public int? Progress { get; set; }
        public int? ProgressVolumes { get; set; }
        public int? Repeat { get; set; }
        public string Notes { get; set; }
        public bool Private { get; set; }
    }
}
