﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Domain.Arguments.Video
{
    public class AdicionarVideoResponse
    {
        public AdicionarVideoResponse(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; set; }
    }
}
